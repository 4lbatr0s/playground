import React from 'react'
import ProductComponent from '../../components/product/ProductComponent'
import Sidebar from '../../components/sidebar/Sidebar'
import Topbar from '../../components/topbar/Topbar'

const Product = () => {
  return (
    <div>
        <Topbar/>
        <div className="container">
            <Sidebar/>
            <ProductComponent/>
        </div>
    </div>
  )
}

export default Product