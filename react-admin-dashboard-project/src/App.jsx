import { useState } from 'react'
import './App.css'
import Sidebar from './components/sidebar/Sidebar'
import Topbar from './components/topbar/Topbar'
import Homepage from './pages/homepage/Homepage'
import UserList from './pages/userlist/UserList'
function App() {

  return (
    <div>
      <Topbar />
      <div className="container">
        <Sidebar/>
        <Homepage/>
      </div>
    </div>
  )
}

export default App
