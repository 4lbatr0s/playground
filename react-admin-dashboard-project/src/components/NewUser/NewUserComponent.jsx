import React from 'react'
import "./NewUser.css";

const NewUserComponent = () => {
    return (
        <div className='newUserComponent'>
            <h1 className="newUserTitle">
                New User
            </h1>
            <form action="" className="newUserForm">
                <div className="newUserFormItem">
                    <label htmlFor="">User Name</label>
                    <input type="text" placeholder='john' />
                </div>
                <div className="newUserFormItem">
                    <label htmlFor="">Full Name</label>
                    <input type="text" placeholder='john smith' />
                </div>
                <div className="newUserFormItem">
                    <label htmlFor="">Email</label>
                    <input type="email" placeholder='john@gmail.com' />
                </div>
                <div className="newUserFormItem">
                    <label htmlFor="">Password</label>
                    <input type="password" placeholder='' />
                </div>
                <div className="newUserFormItem">
                    <label htmlFor="">Phone</label>
                    <input type="tel" pattern='[0-9]{3}-[0-9]{3}-[0-9]{4}' placeholder='532 213 1451' maxLength={11} />
                </div>
                <div className="newUserFormItem">
                    <label htmlFor="">Address</label>
                    <input type="email" placeholder='NYC, USA' />
                </div>
                <div className="newUserFormItem">
                    <label htmlFor="">Gender</label>
                    <div className="newUserGender"> {/*INFO: HOW TO USE SELECT AND OPTION */}
                        <div className="newUserGenderItem"></div>
                        <input type="radio" name="gender" id="male" value="male" />
                        <label htmlFor="male">Male</label>
                        <input type="radio" name="gender" id="female" value="female" />
                        <label htmlFor="female">Female</label>
                        <input type="radio" name="gender" id="other" value="other" />
                        <label htmlFor="other">Other</label>
                    </div>            
                </div>
                <div className='newUserFormItem'>
                    <label htmlFor="">Active</label>
                    <select className='newUserSelect' name="active" id="active">
                        <option value="yes">Yes</option>
                        <option value="no">No</option>
                    </select>
                </div>
                <button className="newUserButton">Create</button>
            </form>
        </div>
    )
}

export default NewUserComponent