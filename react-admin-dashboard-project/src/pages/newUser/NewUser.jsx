import React from 'react'
import "./NewUser.css";
import NewUserComponent from '../../components/NewUser/NewUserComponent';
import Topbar from '../../components/topbar/Topbar';
import Sidebar from '../../components/sidebar/Sidebar';

const NewUser = () => {
  return (
    <div>
      <Topbar />
      <div className='container'>
        <Sidebar />
        <NewUserComponent/>
      </div>
    </div>
  )
}

export default NewUser