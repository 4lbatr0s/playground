import { useState } from 'react'
import './App.css'
import { Link } from 'react-router-dom'

function App() {
  return (
    <div className="App">
      <Link to ="/pay">
        <button>Pay Now</button>
      </Link>
    </div>
  )
}

export default App
