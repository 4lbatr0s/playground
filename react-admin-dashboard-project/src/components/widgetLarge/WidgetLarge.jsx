import React from 'react'
import "./WidgetLarge.css";

const WidgetLarge = () => {

  //INFO: WIDGETLARGE: How to create a button component!
  const Button = ({type}) => {
    return <button className={'widgetLargeButton '+ type}>{type}</button>
  }

  return ( //INFO: WIDGETLARGE: How to create a table!
    <div className='widgetLarge'>
      <h3 className="widgetLargeTitle">Latest Transactions</h3>
      <table className="widgetLargeTable">
        <tr className="widgetLargeTr">
          <th className="widgetLargeTh">Customer</th>
          <th className="widgetLargeTh">Date</th>
          <th className="widgetLargeTh">Amount</th>
          <th className="widgetLargeTh">Status</th>
        </tr>
        <tr className="widgetLargeTr">
          <td className="widgetLargeUser">
            <img src="https://upload.wikimedia.org/wikipedia/tr/6/6b/Gandalf.jpg" alt="" className="widgetLargeImg" />
            <span className="widgetLargeName">Suzan Carol</span>
          </td>
          <td className="widgetLargeDate">2 Jun 2022</td>
          <td className="widgetLargeAmount">$122</td>
          <td className="widgetLargeStatus"><Button type="approved"></Button></td>
        </tr> 
        <tr className="widgetLargeTr">
          <td className="widgetLargeUser">
            <img src="https://upload.wikimedia.org/wikipedia/tr/6/6b/Gandalf.jpg" alt="" className="widgetLargeImg" />
            <span className="widgetLargeName">Suzan Carol</span>
          </td>
          <td className="widgetLargeDate">2 Jun 2022</td>
          <td className="widgetLargeAmount">$122</td>
          <td className="widgetLargeStatus"><Button type="declined"></Button></td>
        </tr> 
        <tr className="widgetLargeTr">
          <td className="widgetLargeUser">
            <img src="https://upload.wikimedia.org/wikipedia/tr/6/6b/Gandalf.jpg" alt="" className="widgetLargeImg" />
            <span className="widgetLargeName">Suzan Carol</span>
          </td>
          <td className="widgetLargeDate">2 Jun 2022</td>
          <td className="widgetLargeAmount">$122</td>
          <td className="widgetLargeStatus"><Button type="pending"></Button></td>
        </tr> 
      </table>
    </div>
  )
}

export default WidgetLarge
