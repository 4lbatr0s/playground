import { Visibility } from '@mui/icons-material';
import React from 'react'
import "./WidgetSmall.css";


const WidgetSmall = () => {
  return (
    <div className='widgetSmall'>
      <span className="widgetSmallTitle">New Join Members</span>
        <ul WidgetSmallList className='widgetSmallList'>
            <li className="widgetSmallListItem">
                <img src="https://upload.wikimedia.org/wikipedia/tr/6/6b/Gandalf.jpg" alt="" className="widgetSmallImage" />
                <div className="widgetSmallUser">
                    <span className="WidgetSmallUserName">Anna Keller</span>
                    <span className="WidgetSmallJobTitle">Software Engineer</span>
                </div> 
                <button className="widgetSmallButton">
                    <Visibility/>
                    Display
                </button>
            </li>
            <li className="widgetSmallListItem">
                <img src="https://upload.wikimedia.org/wikipedia/tr/6/6b/Gandalf.jpg" alt="" className="widgetSmallImage" />
                <div className="widgetSmallUser">
                    <span className="WidgetSmallUserName">Anna Keller</span>
                    <span className="WidgetSmallUserTitle">Software Engineer</span>
                </div> 
                <button className="widgetSmallButton">
                    <Visibility/>
                    Display
                </button>
            </li>
            <li className="widgetSmallListItem">
                <img src="https://upload.wikimedia.org/wikipedia/tr/6/6b/Gandalf.jpg" alt="" className="widgetSmallImage" />
                <div className="widgetSmallUser">
                    <span className="WidgetSmallUserName">Anna Keller</span>
                    <span className="WidgetSmallJobTitle">Software Engineer</span>
                </div> 
                <button className="widgetSmallButton">
                    <Visibility/>
                    Display
                </button>
            </li>
            <li className="widgetSmallListItem">
                <img src="https://upload.wikimedia.org/wikipedia/tr/6/6b/Gandalf.jpg" alt="" className="widgetSmallImage" />
                <div className="widgetSmallUser">
                    <span className="WidgetSmallUserName">Anna Keller</span>
                    <span className="WidgetSmallJobTitle">Software Engineer</span>
                </div> 
                <button className="widgetSmallButton">
                    <Visibility className='widgetSmallIcon'/>
                    Display
                </button>
            </li>
            <li className="widgetSmallListItem">
                <img src="https://upload.wikimedia.org/wikipedia/tr/6/6b/Gandalf.jpg" alt="" className="widgetSmallImage" />
                <div className="widgetSmallUser">
                    <span className="WidgetSmallUserName">Anna Keller</span>
                    <span className="WidgetSmallJobTitle">Software Engineer</span>
                </div> 
                <button className="widgetSmallButton">
                    <Visibility/>
                    Display
                </button>
            </li>
        </ul>
    </div>
  )
}

export default WidgetSmall
