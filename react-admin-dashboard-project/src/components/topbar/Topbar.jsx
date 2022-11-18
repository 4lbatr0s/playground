import React from 'react'
import "./Topbar.css";
import {NotificationsNone, Language, Settings} from '@mui/icons-material';
//
const Topbar = () => {
  return (
    <div className='topbar'>
        <div className="topbarWrapper">
            <div className="topLeft">
                <span className='logo'>UserAdmin</span>
            </div>
            <div className="topRight">
                <div className="topbarIconContainer">
                    <NotificationsNone />
                    <span className='topIconBadge'>2</span>
                </div>
                <div className="topbarIconContainer">
                    <Language />
                    <span className='topIconLanguage'></span>
                </div>
                <div className="topbarIconContainer">
                    <Settings />
                    <span className='topIconSettings'></span>
                </div>
                <img src="https://i.kym-cdn.com/entries/icons/mobile/000/012/982/039.jpg" alt="brent-rambo-approving" className="topAvatar" />
            </div>
        </div>
    </div>
  )
}

export default Topbar
