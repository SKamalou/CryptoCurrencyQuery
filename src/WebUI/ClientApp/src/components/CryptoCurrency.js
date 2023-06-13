import React, { useState, useEffect } from 'react';

export function CryptoCurrency() {
	const [cryptos, setCryptos] = useState([]);
	const [lastSelectedCrypto, setLastSelectedCrypto] = useState('');
	const [quotes, setQuotes] = useState([]);

	useEffect(() => {
		async function fetchCryptos() {
      const response = await fetch('api/cryptocurrencies');
      const cryptosData = await response.json();

      if (!cryptosData.success) {
        console.error(cryptosData.data);
        return;
      }

      setCryptos(cryptosData.data);
		}

		fetchCryptos();
	}, []);

  useEffect(() => {
		async function fetchLastSelectedCrypto() {
      const response = await fetch('api/cryptocurrencies/lastSelected');
      const lastSelectedCryptoData = await response.json();

      if (!lastSelectedCryptoData.success) {
        console.error(lastSelectedCryptoData.data);
        return;
      }

      const lastSelectedCrypto = lastSelectedCryptoData.data;

      if (cryptos.find(opt => opt.symbol == lastSelectedCrypto) != null)
        setLastSelectedCrypto(lastSelectedCrypto);
		}

		fetchLastSelectedCrypto();
  }, [cryptos]);

	useEffect(() => {
    async function fetchData() {
      setQuotes([]);

      const response = await fetch('api/cryptocurrencies/quotes/' + lastSelectedCrypto);
      const quotesData = await response.json();

      if (!quotesData.success) {
        console.error(quotesData.data);
        return;
      }

      setQuotes(quotesData.data);
		}

		if (lastSelectedCrypto) {
			fetchData();
		}
	}, [lastSelectedCrypto]);

	function handleSelectChange(event) {
    setLastSelectedCrypto(event.target.value);
	}

	return (
		<div>
			CryptoCurrency: <select value={lastSelectedCrypto} onChange={handleSelectChange}>
				<option value="">Select an crypto</option>
        {cryptos.map(crypto => (
          <option key={crypto.symbol} value={crypto.symbol}>{crypto.symbol}</option>
				))}
			</select>
			<br/>
			<br/>
			{quotes && (
      <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>Currency</th>
            <th>Price</th>
          </tr>
        </thead>
        <tbody>
          {quotes.map(item =>
            <tr key={item.symbol.symbol}>
              <td>{item.symbol.symbol}</td>
              <td>{item.quote.price}</td>
            </tr>
          )}
        </tbody>
      </table>
			)}
		</div>
	);
}