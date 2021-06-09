import React, { useEffect, useState } from 'react';
import { useHistory, useParams, Link } from 'react-router-dom';

export function CompraForm() {
  const [item, setItem] = useState("");
  const [pessoaId, setPessoaId] = useState(0);
  const [valor, setValor] = useState(0);
  const [formaPagamento, setFormaPagamento] = useState("");
  const [pessoas, setPessoas] = useState([]);

  const history = useHistory();
  const params = useParams();

  const fichaId = params.fichaId;
  const id = params.id;

  useEffect(() => {
    fetchPessoas();

    if (id)
      fetchCompra()
  }, []);

  async function fetchCompra() {
    const res = await fetch(`${process.env.REACT_APP_API_URL}/compras/${id}`);
    const data = await res.json();

    setItem(data.item);
    setPessoaId(data.pessoaId);
    setValor(data.valor);
    setFormaPagamento(data.formaPagamento);
  }

  async function fetchPessoas() {
    const res = await fetch(`${process.env.REACT_APP_API_URL}/pessoas`);
    const data = await res.json();

    setPessoas(data);
  }

  async function createCompra(e) {
    e.preventDefault();

    await fetch(`${process.env.REACT_APP_API_URL}/compras`, {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ item, pessoaId, fichaId, valor, formaPagamento: parseInt(formaPagamento) })
    });

    history.push(`/fichas/${fichaId}`);
  }

  async function editCompra(e) {
    e.preventDefault();

    await fetch(`${process.env.REACT_APP_API_URL}/compras/${id}`, {
      method: 'PUT',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ id, item, pessoaId, fichaId, valor, formaPagamento: parseInt(formaPagamento) })
    });

    history.push(`/fichas/${fichaId}`);
  }

  return (
    <div>
      <Link to={`/fichas/${fichaId}`}>{`< Voltar para ficha #${fichaId}`}</Link>
      <h1 id="tabelLabel">{id ? `Editar compra #${id} da ficha #${fichaId}` : `Criar compra na ficha #${fichaId}`}</h1>

      <form onSubmit={(e) => id ? editCompra(e) : createCompra(e)}>
        <div className="form-group">
          <label htmlFor="pessoa">Quem</label>
          <select className="form-control" id="pessoa" value={pessoaId} onChange={(e) => setPessoaId(e.target.value)} required>
            <option>Selecione quem fez a compra</option>
            {pessoas.length > 0 && pessoas.map(pessoa => <option key={pessoa.id} value={pessoa.id}>{pessoa.nome}</option>)}
          </select>
        </div>
        <div className="form-group">
          <label htmlFor="item">O que</label>
          <input type="text" className="form-control" id="item" placeholder="Digite a compra" value={item} onChange={(e) => setItem(e.target.value)} />
        </div>
        <div className="form-group">
          <label htmlFor="valor">Valor</label>
          <input type="number" min="1" step="any" className="form-control" id="valor" placeholder="Digite o valor" value={valor} onChange={(e) => setValor(e.target.value)} />
        </div>
        <div className="form-group">
          <label htmlFor="formaPagamento">Forma de pagamento</label>
          <select className="form-control" id="formaPagamento" value={formaPagamento} onChange={(e) => setFormaPagamento(e.target.value)} required>
            <option>Selecione a forma de pagamento</option>
            <option value="0">CREDITO</option>
            <option value="1">DEBITO</option>
            <option value="2">DINHEIRO</option>
            <option value="3">VALE</option>
          </select>
        </div>
        
        <button type="submit" className="btn btn-primary">{id ? "Salvar" : "Criar"}</button>
      </form>
    </div>
        
  );
}
