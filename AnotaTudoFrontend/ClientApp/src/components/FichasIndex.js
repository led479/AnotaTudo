import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';

export function FichasIndex() {
  const [fichas, setFichas] = useState([]);
  const [showFichaCreate, setShowFichaCreate] = useState(false);
  const [dataNovaFicha, setDataNovaFicha] = useState(null);
  const [loading, setLoading] = useState(true);

  const history = useHistory();

  const realBR = Intl.NumberFormat('pt-BR', {
    style: "currency",
    currency: "BRL",
  });

  useEffect(() => {
    populateFichas()
  }, [])

  async function populateFichas() {
    const res = await fetch(`${process.env.REACT_APP_API_URL}/fichas`);
    const data = await res.json();
    setFichas(data);
    setLoading(false);
  }

  async function createFicha(e, data = null) {
    e.preventDefault();

    let now = new Date();
    now.setHours(now.getHours() - 3);

    await fetch(`${process.env.REACT_APP_API_URL}/fichas`, {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ data: data || now })
    });

    setShowFichaCreate(false);
    populateFichas();
  }

  return (
    <div>
      <h1 id="tabelLabel" >Fichas</h1>
      <p id="text-muted" >Pressione a ficha para acessar suas compras</p>
      { !showFichaCreate &&
        <button className="btn btn-primary p-2 mb-4" onClick={(e) => setShowFichaCreate(!showFichaCreate)}>Criar ficha</button>
      }
      { showFichaCreate &&
      <div>
        <button className="btn btn-primary p-2 mb-4" onClick={(e) => createFicha(e)}>Criar ficha de hoje</button>
        <form className="form-inline mb-2" onSubmit={(e) => createFicha(e, dataNovaFicha)}>
          <div className="form-group">
            <label htmlFor="data" className="mr-2">Data</label>
            <input type="date" className="form-control mr-2" id="data" onInput={(e) => setDataNovaFicha(e.target.value)} />
          </div>
          <button type="submit" className="btn btn-primary">Criar ficha da data preenchida</button>
        </form>
      </div>}
      <div className="card-deck">
      { loading ?
        <p><em>Loading...</em></p>
        :
          fichas.map(ficha => (
            <div className="card mb-4" style={{ minWidth: "18rem", maxWidth: "18rem", cursor: "pointer" }} key={ficha.id} onClick={(e) => history.push(`fichas/${ficha.id}`)}>
              <div className="card-body d-flex flex-column">
                <h5 className="card-title">{ficha.dataString}</h5>
                <h6 className="card-subtitle mb-2 text-muted">Ficha #{ficha.id}</h6>
                <ul className="card-text">{ficha.compras.map(compra => <li key={compra.id}>{compra.item}, {realBR.format(compra.valor)}</li>)}</ul>
              </div>

            </div>
        ))
      }
      </div>
        
    </div>
  );
}
