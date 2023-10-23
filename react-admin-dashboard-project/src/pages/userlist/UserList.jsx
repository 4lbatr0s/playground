import React from 'react'
import Sidebar from '../../components/sidebar/Sidebar';
import Topbar from '../../components/topbar/Topbar';
import UserListComp from '../../components/userList/userList';
import "./UserList.css";

const UserList = () => {
  //INFO: How to use Material UI Data table!

  return (
    <div>
      <Topbar />
      <div className='container'>
        <Sidebar />
        <UserListComp/>
      </div>
    </div>
  )
}

export default UserList