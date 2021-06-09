import React, { useState, useEffect } from 'react';
import { useHistory } from 'react-router-dom';

export function PessoasIndex() {
  const [pessoas, setPessoas] = useState();
  const [loading, setLoading] = useState(true);

  const history = useHistory();

  async function populatePessoas() {
    const res = await fetch(`${process.env.REACT_APP_API_URL}/pessoas`);
    const data = await res.json();
    setPessoas(data);
    setLoading(false);
  }

  useEffect(() => {
    populatePessoas()
  }, [])

  async function removePessoa(e, id) {
    e.preventDefault();

    await fetch(`${process.env.REACT_APP_API_URL}/pessoas/${id}`, {
      method: 'DELETE'
    });

    populatePessoas();
  }

  return (
    <div>
      <h1 id="tabelLabel" >Pessoas</h1>
      <div className="card-deck p-2">
      { loading ?
        <p><em>Loading...</em></p>
        :
        pessoas.map(pessoa => (
            <div className="card" style={{ width: "18rem" }} key={pessoa.id}>
              <div className="card-body">
                <h5 className="card-title">Pessoa #{ pessoa.id }</h5>
                <h5 className="card-subtitle mb-2 text-muted">{pessoa.nome}</h5>

                {/*<ul className="card-text">{pessoa.compras.slice(0, 3).map(compra => <li key={compra.id}>{compra.item}, {realBR.format(compra.valor)}</li>)}</ul>*/}
                <button className="btn-sm btn-danger" onClick={(e) => removePessoa(e, pessoa.id)}>Remover</button>
              </div>
            </div>
        ))
        }
        
      </div>
      <button className="btn btn-primary p-2 ml-2" onClick={() => history.push("/pessoas/new")}> Adicionar pessoa</button>
        
    </div>
  );
}
