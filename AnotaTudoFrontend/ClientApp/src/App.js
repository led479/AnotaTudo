import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { FichasIndex } from './components/FichasIndex';
import { CompraForm } from './components/CompraForm';
import { PessoasIndex } from './components/PessoasIndex';
import { PessoaForm } from './components/PessoaForm';
import { FichaIndex } from './components/FichaIndex';

import './custom.css'

export default class App extends Component {
  static displayName = App.name;

  render () {
    return (
      <Layout>
        <Route exact path='/' component={FichasIndex} />
        <Route exact path='/fichas/:id' component={FichaIndex} />
        <Route exact path='/fichas/:fichaId/compras/new' component={CompraForm} />
        <Route exact path='/fichas/:fichaId/compras/:id/edit' component={CompraForm} />
        <Route exact path='/pessoas' component={PessoasIndex} />
        <Route exact path='/pessoas/:id/edit' component={PessoaForm} />
        <Route exact path='/pessoas/new' component={PessoaForm} />
      </Layout>
    );
  }
}
