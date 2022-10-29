import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'
import { createBrowserRouter, RouterProvider, Route } from 'react-router-dom'
import Pay from './pages/Pay'
import Success from './pages/Success'

const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
  },
  {
    path: "pay", 
    element: <Pay />,
  },
  {
    path: "success",
    element: <Success />
  }
])


ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <RouterProvider router={router}></RouterProvider>
  </React.StrictMode>
)
