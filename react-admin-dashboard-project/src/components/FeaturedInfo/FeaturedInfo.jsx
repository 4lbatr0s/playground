import React from 'react'
import "./FeaturedInfo.css";
import {ArrowDownward, ArrowOutward, ArrowUpward} from '@mui/icons-material';

export default function FeaturedInfo(){
  return (
    <div className='featuredInfo'>
        <div className="featuredItem">
            <span className="featuredTitle">Revenue</span>
            <div className="featuredMoneyContainer">
                <span className="featuredMoney">$2,451</span>
                <span className="featuredMoneyRate">
                    -11.4 <ArrowDownward className='featuredIcon negative'/>
                    </span>
            </div>
            <span className="featuredSub">Compared to last month:</span>
        </div>

        <div className="featuredItem">
            <span className="featuredTitle">Sales</span>
            <div className="featuredMoneyContainer">
                <span className="featuredMoney">$2,225</span>
                <span className="featuredMoneyRate">
                    +2.4 <ArrowUpward className='featuredIcon positive'/>
                    </span>
            </div>
            <span className="featuredSub">Compared to last month:</span>
        </div>

        <div className="featuredItem">
            <span className="featuredTitle">Cost</span>
            <div className="featuredMoneyContainer">
                <span className="featuredMoney">$2,225</span>
                <span className="featuredMoneyRate">
                    +2.4 <ArrowUpward className='featuredIcon positive'/>
                    </span>
            </div>
            <span className="featuredSub">Compared to last month:</span>
        </div>
    </div>
  )
}

