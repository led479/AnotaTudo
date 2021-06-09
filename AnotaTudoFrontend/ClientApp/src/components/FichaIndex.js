import React, { useState, useEffect } from 'react';
import { useHistory, useParams, Link } from 'react-router-dom';

export function FichaIndex() {
  const [ficha, setFicha] = useState();
  const [loading, setLoading] = useState(true);

  const history = useHistory();
  const params = useParams();


  const id = params.id; //FROM URL

  const realBR = Intl.NumberFormat('pt-BR', {
    style: "currency",
    currency: "BRL",
  });

  useEffect(() => {
    populateFicha()
  }, [id])

  async function populateFicha() {
    const res = await fetch(`${process.env.REACT_APP_API_URL}/fichas/${id}`);
    const data = await res.json();
    setFicha(data);
    setLoading(false);
  }

  

  async function removeCompra(e, compraId) {
    e.preventDefault();

    await fetch(`${process.env.REACT_APP_API_URL}/compras/${compraId}`, { method: 'DELETE' });

    populateFicha()
  }

  return (
    <div>
      <Link to="/fichas">{"< Voltar para fichas"}</Link>
      <h1 id="tabelLabel" >Ficha #{id} - { loading ? "" : ficha.dataString }</h1>
      <div className="d-flex flex-row">
      { loading ?
          <p><em>Loading...</em></p>
        :
          <table className="table">
            <thead>
              <tr>
                <th scope="col">#</th>
                <th scope="col">Quem</th>
                <th scope="col">O que</th>
                <th scope="col">Valor</th>
                <th scope="col">Forma de pagamento</th>
                <th scope="col" style={{ width: "10rem" }}></th>
              </tr>
            </thead>
            <tbody>
              {ficha.compras.map(compra => (
                <tr key={compra.id}>
                  <th scope="row">{compra.id}</th>
                  <td>{compra.pessoa.nome}</td>
                  <td>{compra.item}</td>
                  <td>{realBR.format(compra.valor)}</td>
                  <td>{compra.formaPagamentoString}</td>
                  <td><button className="btn-sm btn-primary pr-2" onClick={() => history.push(`/fichas/${id}/compras/${compra.id}/edit`)}>Editar</button> <button className="btn-sm btn-danger" onClick={(e) => removeCompra(e, compra.id)}> Remover</button></td>
                </tr>
                ))
              }
                
            </tbody>
          </table>
      }
      </div>
      <button className="btn btn-primary p-2" onClick={() => history.push(`/fichas/${id}/compras/new`)}>Adicionar compra</button>
        
    </div>
  );
}
