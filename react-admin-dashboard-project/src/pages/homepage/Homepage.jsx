import FeaturedInfo from "../../components/FeaturedInfo/FeaturedInfo";
import Chart from "../../components/chart/Chart";
import { userData } from "../../dummyData";
import "./Homepage.css";
import WidgetSmall from "../../components/widgetSmall/WidgetSmall";
import WidgetLarge from "../../components/widgetLarge/WidgetLarge";

export default function Homepage() {
  return (
    <div className="home">
      <FeaturedInfo/> {/*INFO: How to use chart data. */}
      <Chart data={userData} title="User Analytics" grid dataKey="Active User"/>
      <div className="homeWidgets">
        <WidgetSmall></WidgetSmall>
        <WidgetLarge></WidgetLarge>
      </div>
    </div>
  );
}