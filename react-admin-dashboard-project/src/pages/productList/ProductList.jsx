import React from 'react';
import ProductListComponent from '../../components/ProductList/ProductListComponent.jsx';
import Sidebar from '../../components/sidebar/Sidebar.jsx';
import Topbar from '../../components/topbar/Topbar.jsx';
import "./ProductList.jsx";

const ProductList = () => {
  return (
    <div>
        <Topbar/>
        <div className='container'>
            <Sidebar/>
            <ProductListComponent/>
        </div>
    </div>
  )
}

export default ProductList