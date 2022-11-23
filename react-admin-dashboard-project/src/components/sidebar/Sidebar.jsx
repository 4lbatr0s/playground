import React from 'react'
import './Sidebar.css';
import {AttachMoneyOutlined, BarChartOutlined, EmailOutlined, ErrorOutline, ErrorOutlineOutlined, FeedbackOutlined, LineStyle, MessageOutlined, Person2Outlined, StorefrontOutlined, Timeline, TrendingUp, WorkOutline} from '@mui/icons-material/';
const Sidebar = () => {
  return (
    <div className='sidebar'>
        <div className="sidebarWrapper">
            <div className="sidebarMenu">
                <h3 className="sidebarTitle">Dashboard</h3>
                <ul className="sidebarList">
                    <li className="sidebarListItem">
                        <LineStyle className="sidebarIcon"/>
                        Home
                    </li>
                    <li className="sidebarListItem">
                        <Timeline className="sidebarIcon"/>
                        Analytics
                    </li>
                    <li className="sidebarListItem">
                        <TrendingUp className="sidebarIcon"/>
                        Sales
                    </li>
                </ul>
            </div>
            <div className="sidebarMenu">
                <h3 className="sidebarTitle">Quick Menu</h3>
                <ul className="sidebarList">
                    <li className="sidebarListItem">
                        <Person2Outlined className="sidebarIcon"/>
                        Users
                    </li>
                    <li className="sidebarListItem">
                        <StorefrontOutlined className="sidebarIcon"/>
                        Products
                    </li>
                    <li className="sidebarListItem">
                        <AttachMoneyOutlined className="sidebarIcon"/>
                        Transactions
                    </li>
                    <li className="sidebarListItem">
                        <BarChartOutlined className="sidebarIcon"/>
                        Reports
                    </li>
                </ul>
            </div>
            <div className="sidebarMenu">
                <h3 className="sidebarTitle">Notifications</h3>
                <ul className="sidebarList">
                    <li className="sidebarListItem">
                        <EmailOutlined className="sidebarIcon"/>
                        Mail
                    </li>
                    <li  className="sidebarListItem">
                        <FeedbackOutlined className="sidebarIcon"/>
                        Feedback
                    </li>
                    <li className="sidebarListItem">
                        <MessageOutlined className="sidebarIcon"/>
                        Messages
                    </li>
                </ul>
            </div>
            <div className="sidebarMenu">
                <h3 className="sidebarTitle">Staff</h3>
                <ul className="sidebarList">
                    <li className="sidebarListItem">
                        <WorkOutline className="sidebarIcon"/>
                        Home
                    </li>
                    <li className="sidebarListItem">
                        <Timeline className="sidebarIcon"/>
                        Analytics
                    </li>
                    <li className="sidebarListItem">
                        <ErrorOutline className="sidebarIcon"/>
                        Reports
                    </li>
                </ul>
            </div>
        </div>
    </div>
  )
}

export default Sidebar