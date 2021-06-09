import React, { useState } from 'react';
import { useHistory, Link } from 'react-router-dom';

export function PessoaForm() {
  const [nome, setNome] = useState();

  const history = useHistory();

  async function createPessoa(e) {
    e.preventDefault();

    await fetch(`${process.env.REACT_APP_API_URL}/pessoas`, {
      method: 'POST',
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ nome })
    });

    history.push("/pessoas");

  }

  return (
    <div>
      <Link to="/pessoas">{"< Voltar para pessoas"}</Link>
      <h1 id="tabelLabel">Criar pessoa</h1>

      <form onSubmit={(e) => createPessoa(e) }>
        <div className="form-group">
          <label htmlFor="nome">Nome</label>
          <input type="text" className="form-control" id="nome" placeholder="Digite o nome" onInput={(e) => setNome(e.target.value)} />
        </div>
        <button type="submit" className="btn btn-primary">Criar</button>
      </form>
    </div>
        
  );
}
