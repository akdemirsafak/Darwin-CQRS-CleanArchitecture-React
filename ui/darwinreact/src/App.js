import './App.css';
import {useRoutes, generatePath } from'react-router-dom';
import routes from './routes';

function App() {


  console.log(generatePath(''))
  // return (
  //   <div className="App">
  
  //   </div>
  // );
  return useRoutes(routes);
}

export default App;
