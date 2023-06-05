import React, { useState, useEffect } from 'react';

export function CryptoCurrency() {
	const [options, setOptions] = useState([]);
	const [selectedCrypto, setSelectedCrypto] = useState('');
	const [quotes, setQuotes] = useState([]);

	useEffect(() => {
		async function fetchOptions() {
      const response = await fetch('api/cryptocurrencies');
      const optionsData = await response.json();

      if (!optionsData.success) {
        console.error(optionsData.data);
        return;
      }

      setOptions(optionsData.data);
		}

		fetchOptions();
	}, []);

  useEffect(() => {
		async function fetchLastSelectedCrypto() {
      const response = await fetch('api/cryptocurrencies/selected');
      const selectedCryptoData = await response.json();

      if (!selectedCryptoData.success) {
        console.error(selectedCryptoData.data);
        return;
      }

      const lastSelectedCrypto = selectedCryptoData.data;

      if (options.find(opt => opt.symbol == lastSelectedCrypto) != null)
				setSelectedCrypto(lastSelectedCrypto);
		}

		fetchLastSelectedCrypto();
  }, [options]);

	useEffect(() => {
    async function fetchData() {
      setQuotes([]);

      const response = await fetch('api/cryptocurrencies/quotes/' + selectedCrypto);
      const quotesData = await response.json();

      if (!quotesData.success) {
        console.error(quotesData.data);
        return;
      }

      setQuotes(quotesData.data);
		}

		if (selectedCrypto) {
			fetchData();
		}
	}, [selectedCrypto]);

	function handleSelectChange(event) {
		setSelectedCrypto(event.target.value);
	}

	return (
		<div>
			CryptoCurrency: <select value={selectedCrypto} onChange={handleSelectChange}>
				<option value="">Select an option</option>
				{options.map(option => (
					<option key={option.symbol} value={option.symbol}>{option.symbol}</option>
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