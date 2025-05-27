import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import InvitedPage from './pages/InvitedPage';
import AcceptedPage from './pages/AcceptedPage';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<InvitedPage />} />
        <Route path="/accepted" element={<AcceptedPage />} />
      </Routes>
    </Router>
  );
}

export default App;
