import React from 'react'
import "./UserDetail.css";
import {Publish, CalendarToday, LocationSearching, MailOutline, PermIdentity, PhoneAndroid } from '@mui/icons-material';
import { Link } from 'react-router-dom';
const UserDetail = () => {
  return (
    <div className='userDetail'>
      <div className="userTitleContainer">
        <h1 className="userTitle">Edit User</h1>
        <Link to ="/newUser">
          <button className="userAddButton">Create</button>
        </Link>
      </div>
      <div className="userContainer">
        <div className="userShow">
            <div className="userShowTop">
              <img className="userShowTopImg" src="https://nypost.com/wp-content/uploads/sites/2/2020/12/yael-most-beautiful-video.jpg?quality=75&strip=all" alt=""/>
            <div className="userShowTopTitle">
              <span className="userShowUserName">Anna Backer</span>
              <span className="userShowTitle">Software Engineer</span>
            </div>
          </div>
          <div className="userShowBottom">
            <span className="userShowTitle">Account Details</span>
            <div className="userShowInfo">
              <PermIdentity className="userShowIcon"/>
              <span className="userShowInfoTitle">annabeck99</span>  
            </div>
            <div className="userShowInfo">
              <CalendarToday className="userShowIcon"/>
              <span className="userShowInfoTitle">10.12.1999</span>  
            </div>
            <span className="userShowTitle">Contact Details</span>
            <div className="userShowInfo">
              <PhoneAndroid className="userShowIcon"/>
              <span className="userShowInfoTitle">+9015125125912</span>  
            </div>
            <div className="userShowInfo">
              <MailOutline className="userShowIcon"/>
              <span className="userShowInfoTitle">annabeck99@gmail.com</span>  
            </div>
            <div className="userShowInfo">
              <LocationSearching className="userShowIcon"/>
              <span className="userShowInfoTitle">NYC, USA</span>  
            </div>
          </div>
        </div>
        <div className="userUpdate">
          <div className="userUpdateTitle">
            Edit
          </div>
          <form action="" className="userUpdateForm">
            <div className="userUpdateLeft">
              <div className="userUpdateItem">
                <label htmlFor="">Username</label>
                <input type="text" placeholder='annabeck99' className='userUpdateInput' />
              </div>
              <div className="userUpdateItem">
                <label htmlFor="">Full Name</label>
                <input type="text" placeholder='Anna Becker' className='userUpdateInput' />
              </div>
              <div className="userUpdateItem">
                <label htmlFor="">Email</label>
                <input type="text" placeholder='annabeck99@gmail.com' className='userUpdateInput' />
              </div>
              <div className="userUpdateItem">
                <label htmlFor="">Phone</label>
                <input type="text" placeholder='12842194912' className='userUpdateInput' />
              </div>
              <div className="userUpdateItem">
                <label htmlFor="">Address</label>
                <input type="text" placeholder='NYC, USA' className='userUpdateInput' />
              </div>
            </div>
            <div className="userUpdateRight">
              <div className="userUpdateUpload">
                <img 
                className="userUpdateImg"
                src="https://i.kym-cdn.com/entries/icons/mobile/000/012/982/039.jpg" alt="brentramboapproves"/>
                <label  htmlFor="file">  {/**INFO: HOW TO USE HTMLFOR  */}
                  <Publish className = "userUpdateIcon" />
                </label>
                <input style={{display:"none"}} type="file" name="" id="file" />
              </div>
              <button className="userUpdateButton">Update</button>
            </div>
          </form>
        </div>
      </div>
    </div>
  )
}

export default UserDetail