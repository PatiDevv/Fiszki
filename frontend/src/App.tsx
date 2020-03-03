import React, { useEffect, useState } from "react";
import Axios from "axios";
import { Fiszka } from "./models/Fiszka";
import { Input, Button, DialogTitle } from "@material-ui/core";
import { Dialog } from "@material-ui/core";

const API = "https://localhost:5001/api/Fiszki/";

function UstawTwojaOdpowiedz(
  fiszki: Fiszka[],
  fiszka: Fiszka,
  ustawFiszki: React.Dispatch<React.SetStateAction<Fiszka[]>>,
  e: any
) {
  ustawFiszki(
    fiszki.map(x => {
      if (x.odpowiedz === fiszka.odpowiedz) {
        return {
          ...x,
          twojaOdpowiedz: e.target.value
        };
      } else {
        return x;
      }
    })
  );
}

export default function App() {
  const [fiszki, ustawFiszki] = useState<Fiszka[]>([]);
  const [open, setOpen] = useState(false);
  const [isSucced, setIsSucced] = useState<boolean | null>(null);

  useEffect(() => {
    Axios.get<Fiszka[]>(`${API}lista`)
      .then(response => {
        ustawFiszki(response.data);
      })
      .catch(error => console.log(error));
  }, []);

  return (
    <div style={{ flex: 1 }}>
      <h2
        style={{
          justifyContent: "center",
          alignItems: "center",
          textAlign: "center"
        }}
      >
        Lista fiszek
      </h2>
      <div style={{ textAlign: "center" }}>
        {fiszki.map(fiszka => (
          <div style={{ marginBottom: 20 }}>
            <div
              style={{
                display: "flex",
                flexDirection: "row",
                justifyContent: "center"
              }}
            >
              <div style={{ marginRight: 10, fontWeight: "bold" }}>
                Pytanie:
              </div>
              <div>{fiszka.pytanie}</div>
            </div>

            <Input
              onChange={e => {
                UstawTwojaOdpowiedz(fiszki, fiszka, ustawFiszki, e);
              }}
              placeholder="Odpowiedz"
            ></Input>
            <Button
              style={{ marginLeft: 10 }}
              variant="contained"
              color="primary"
              onClick={() => {
                Axios.post(API + "weryfikacja", {
                  pytanie: fiszka.pytanie,
                  odpowiedz: fiszka.twojaOdpowiedz
                })
                  .then(response => {
                    setOpen(true);
                    setIsSucced(response.data);
                  })
                  .catch(error => console.log(error));
              }}
            >
              Sprawdz
            </Button>
          </div>
        ))}
        <Dialog open={open}>
          <DialogTitle>
            {isSucced === true
              ? "Prawidłowa odpowiedz"
              : "Nieprawidłowa odpowiedz"}
          </DialogTitle>
          <Button onClick={() => setOpen(false)} color="primary" autoFocus>
            OK
          </Button>
        </Dialog>
      </div>
    </div>
  );
}
