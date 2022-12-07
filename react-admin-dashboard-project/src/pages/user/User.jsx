import React from 'react'
import Sidebar from '../../components/sidebar/Sidebar';
import Topbar from '../../components/topbar/Topbar';
import UserDetail from '../../components/user/UserDetail';
import "./User.css";


const User = () => {
  return (
    <div>
        <Topbar/>
        <div className='container'>
        <Sidebar/>
        <UserDetail/>
        </div>
    </div>
  )
}

export default User