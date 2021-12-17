import React, { useEffect, useState } from "react";
import "./Form.css"
import CircularProgress from '@mui/material/CircularProgress';

function App() {
  const [ selectedRecommendationOptions, setSelectedRecommendationOptions ] = useState([]);
  const [ recommendationOptions, setRecommendationOptions ] = useState([]);
  const [ wordOptions, setWordOptions ] = useState();
  const [ kOptions, setKOptions ] = useState(5);

  useEffect(() => {
    const renderTableBody = () => {
      return recommendationOptions.map(recommendation => {
        const {page, totalScore, content, location} = recommendation;
        return (
        <tr key={page.pageId}>
          <td>{page.url}</td>
          <td>{totalScore.toFixed(2)}</td>
          <td>{content.toFixed(2)}</td>
          <td>{location.toFixed(2)}</td>
        </tr>
        )
      })}

    setSelectedRecommendationOptions(renderTableBody)
  }, [recommendationOptions]);

  const handleWordChange = (e) => {
    setWordOptions(e.target.value);
  };

  const handleKChange = (e) => {
    setKOptions(e.target.value);
  };

  const recommendWikis = async (e) => {
    e.preventDefault();
    setSelectedRecommendationOptions(
      <CircularProgress />
    )
    let wikis = await fetch(`http://localhost:8000/search?word=${wordOptions}&k=${kOptions}`);
    setRecommendationOptions(await wikis.json());
  };

  return (
    <div>
      <header>
        <h1>Recommendation System</h1>
      </header>
      <form className = "form">

        <div>
          <label>Word:</label>
            <input type="text" onChange={handleWordChange} />
        </div>

        <div>
          <label>Result:</label>
            <input type="number" min={1} max={10} value={kOptions} onChange={handleKChange}>
            </input>
        </div>

        <div>
          <button className ="submitButton" onClick={recommendWikis}>
            Find wiki
          </button>
        </div>
      </form>

      <div className = "form">
        <table>
          <thead>
            <tr>
              <th>Url</th>
              <th>TotalScore</th>
              <th>Content</th>
              <th>Location</th>
            </tr>
          </thead>
          <tbody>
            {selectedRecommendationOptions}
          </tbody>

         </table>
      </div>

    </div>
  );
}

export default App;
