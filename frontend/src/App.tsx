import React from 'react';
import {BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';
import Login from "./pages/Login";
import Register from "./pages/Register";
import Dashboard from './pages/Dashboard';

function App() {
  return (
    <Router>
      <Routes>
        <Route path = "/login" element = {<Login/>}/>
        <Route path = "/register" element = {<Register/>}/>
        <Route path = "/dashboard" element = {<Dashboard/>}/>
      </Routes>
    </Router>
  );
}

export default App;
