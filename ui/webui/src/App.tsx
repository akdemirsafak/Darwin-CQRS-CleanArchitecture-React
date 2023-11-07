import React from 'react';
import './App.css';
import SignIn from './components/Sign/SignIn'
import SignUp from './components/Sign/SignUp';
import Prices from './components/Cards/Prices';
import Navbar from './components/Global/Navbar';


function App() {
  return (
    <div className="App">
      <Navbar/>
      <SignIn/>

    </div>
  );
}

export default App;
