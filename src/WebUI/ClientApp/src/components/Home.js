import React, { Component } from 'react';
import { CryptoCurrency } from "./CryptoCurrency";

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <h1>Crypto Currency</h1>
        <br></br>

        <CryptoCurrency />
      </div>
    );
  }
}
