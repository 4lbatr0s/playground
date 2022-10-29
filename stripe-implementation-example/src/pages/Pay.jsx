import axios from 'axios';
import React, { useEffect } from 'react'
import { useState } from 'react';
import { redirect, useNavigate} from 'react-router-dom';
import StripeCheckout from 'react-stripe-checkout'; //INFO: 1.How to import stripe checkout
const Pay = () => {
    const [stripeToken, setStripeToken] = useState(null); //INFO:3. this will hold the token we get from Stripe.
    const navigate = useNavigate();
    const KEY = "pk_test_51LxdjNIk6AX0xKBBSCEua5tHHkrLG7Rye51oLvaKXdOgwvbFMYnm44QSAhhdi2z1JX2Qzv6vQaYlEXhfJ9sEqgLv00XTvV0ADM"

    const onToken = (token) => {
        setStripeToken(token); //INFO: get token from stripe and set it to state.
    }

    useEffect(() => { //INFO: 5. How to send stripe payment request to web application
        const makeRequest = async () => {
            try {
                const response = await axios.post("http://localhost:5000/api/checkout/payment",
                    {
                        tokenId: stripeToken.id, //INFO: 4.Stripe token that we will send to backend
                        amount: 2000 //payment amount that we will send to backend.
                    });
                console.log(response.data);
                navigate('/success');
            } catch (error) {
                console.log(error);
            }
        }
        stripeToken && makeRequest();
    }, [stripeToken, navigate])

    return (
        <div style={{ display: "flex", alignItems: "center", justifyContent: "center" }}>
            {stripeToken ? (<span>Processing, Please wait...</span>) : (
                <StripeCheckout
                    name="VERITAS CO"
                    image="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAUYAAACbCAMAAAAp3sKHAAAAe1BMVEX///8AAAAYGBjMzMy4uLjk5OTX19ecnJxaWlogICB5eXkHBweSkpKIiIjU1NTj4+P5+flgYGDx8fHExMSwsLDq6upFRUU3NzdUVFRmZma0tLS+vr4TExNtbW3Nzc2Ojo6kpKQ/Pz8wMDBOTk50dHQpKSmBgYEeHh6goKAGx1FxAAAIsElEQVR4nO2c6XYyrRKFxSHGeWijcR5fk/u/wqMCLUMVkDRZ33Gt/fzTpoHeTQFVhdZqAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAgP9HhqJ+QxzSSi974lJ+mPTqEUTLuLclmFKdxbn73RjTLTZl/8rPba4Wn83Jr26sbxfDtCeujXeH7uei87jpdHzvttcjqph4cEyr81ayKD+8iyi2jGHmB0rJprxYfm7HGy3psNXdGKQ873h2Jrva917CVV6ZptS6Nh8oRUbzxcVkvNFnn7v83I/X8sSvblle28Qfd3oN1L0trLIN+e0sXmutthVi+fwkZZwMGzzm+FIyNndOmWK1L1/5puU26co48toYyof98vux85/A0OEt9rTPNzbpr4pbt4tme7t5VmDrKL/7TFDxXrT5/CRl7KbceEfJ+EFdG8/+MQ/nykjw/ShBWLBPYchIDH2rS0dVbm/PhR/Ni6AaVN8yU7zbCaPY72T0xpviIEiZE2Rsp8t4MWT8Fyw5VsPum7q4u/r2q95Qkypv07UHbV4Zazt53Vle88p4H1yr1TbSkzvSOhbskjG8EnXf7T+pE+YryCyjVuxAfRmqOF3GhqzpTdbZDpSUS9E5oU7nlmBPHwyFvaDnllF15Mv6LquMVzVaenKo8QWLBLN3WcueFrFyg9uGyfycXUZVwlplssqo566BrJRc7R5Ik07doUvGstJtrNzGMbjsMtbmjxLWrJ5Txrs5PZbIHTV9OAWTXZKS9ySrfnPfX34Z5U5taX6VU8ZBOdvJSueBgpG5k2Ila22ES/Xd2SS/jLIj1lqdU8ZNuUR2Za3cOiytIskjMZiKFEE67vvJLyOhWUYZ357CRVy38FWWc4JVjzwJXk3GvjHdyVo5101ejbg5PjN5X3Blans9fTUZO8bqpXbgjOsmL/50ial9yPuCsaOF93peTMa7OZVLpNrkMa6bnBsFGVgMcXzc1guUuCvthA1eTMa2tZ+W1TKu2/6Xw1HFBQKxo4MfoXsxGU+WOU1C9Spv0d56JdCKTqpz3+ZfS8YP20pVvYzrdlI6zpOi2U+k98M7kfc9kbsCvZaMN3OqGx/Drpsejj/d9agUBzupzohHeS0ZXXP6DFb8Xeq4+IlnPZL3sP7PrVEvwPZXXsyfyOiZk9rkca7boNRRvP9gxa5L6ZmrY6rB/DLKFdJydXPJ6JmTct28wWH3RbKM5m2cu5jYUZN6kvdwP1wSZOw8Sli2l0vGT++FzyM1m3kb8Zlo2mpSPdBXl8JI82ukjPN232TPBjjiMqqO/0W8kTAntcnj9RlbGeRzmpDCNyjror81oPPU7PQal3FDdCGTjIQ5JbhuRd18sjmRsPUIpf0L8kFyy6jWTntCzyTjhNjbLB73hVy3ciFSHKMZAh0RJvdJF3JDL2U87gcmV7almIzqrTguQCYZbwXW9H2xtL9O/EpOXi1ES4KJHQnSic+6xLypfL/r5eaRcU1VMSLfm8+sZwr5tQqXVhFhIna0o7/OeGpiqM/WeAtZHhm3ZBSikzCMZSc6ppC9oGfDp/1v0+Y7ccPvZPTO8Ky/t2UP/cbzyHi7TgwitclL2V2vT8JgEzJtWYSYBAU9Zf5OxgDUQY8sMjqnZjQJaX+jDkvII6/9lumxkcOwyCpjr0tvOLPI2GUOQMi6A2l/i8bc7PCBK8al/fdMJ3LJON+3CzbznkVG7qmjaX+H4afRbfa0jrzs7a/qTCd+J6Me19Nd6bWGrCqHjDvGnFRaXxzCHTcZGUL+YzI5dETYymGYVA9N6HAUm3fPI+OVzQdEm/d5e5o2MxuoLjuTlJfm12SI8LTUloyPF+eQscdGXwe2haSxKnWkHUkVEXaEOXFWlyNQNlWnMdnsUQYZ70vkpkeiWv9hYn9aHrKm12sqIvzBls4Sb1QBAnJjeieDjGbkkIZrnEWfracPQFJp/2/W5PKEbXVYj4m0ZJDxKypjyoltG73SkK7tlHiiI+t1Zop+6zdLewbVZbwvkcUbgxo4CSe2bdT8Jw7kVbkKmbGju7JMDCRXEmGhukRO9NVl9E/NmMjqf5qQLg2Xziz6af8ZH5HLJaOyAXo/UF3GUzCIw7luMdRwpPdKasI3mj3z8eFsKS3lPpENVZaRODVDtB2Px7pIG6qHLj67dBedS0Lkywzqn6sQbnVlGb8jAe6QcYbohmR00/6rwCNkTLDqeJ6/YlaW8Rg5KRc8zBNtldnvumn/SUCnjDLqsIUfe68qI3VqhmogcmLbRzqy3Nokx0U524cayJnu16kjL+RYVUbq1IyFWiuScyGavT3eHNTPNlUsgsxhaLKemlC25TlMVWU8RxUKH+ZhOT7u4hYvO+2/pdL8mryHT5R76y4HFWUkT83YpJzY9pFPw/8oW8Zc1H5IhLYCeWVkjmZWlDG0RCoo1y3OxBxtBGbsqBHsROajUPrvD+zcU0UZ30PmpJh7A2sc9bFnsW6piPAjdtQN/rA194kyHX6yYsTVZBwHzUnhu24HcQnPBCqeEiokS5yL9fpeOOC055ZRu/tWQKmajMElUuO7bo95esD7PrOEecD+n4rA8M5+vlGdf7GW1moyLpOiDq7rpmN3X30y0DpVZxPCNTdMFUN/QZH/fKMOsBp2WE3GsDnZFTz3Wsa/q9SvTScPNdSea8x/NGUMhdd/9z88wb8mOKr7njGzSjLSJ+E8lOv23PqPm+az1bftWTFstVrDdfv5BzTRxIP5vxahZM8fyKhjZs/4UyUZL4n5AfXHEtZ364sIMIjHy43TusHfKZF/mGRjzi2jBBnLtstSxKl6lz5bRJBHd3z2dNdGh+VGEMxnSUmHYRljD54nGHGR+ZKh2dx46H1F4N44lXeFbvmQ/fUv3FqMNWc1QpnedHe4Thb61O3Xcdsufpy4AQAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAPwH/A+nKl9Jygw7/AAAAABJRU5ErkJggg=="
                    billingAddress
                    shippingAddress
                    description='Your total is 20$'
                    amount={2000}
                    token={onToken}
                    stripeKey={KEY}
                > {/*INFO: 2.How to cover button with stripe checkout  */}
                    <button type="button" style={{ cursor: "pointer", backgroundColor: "black", border: "none", borderRadius: "10px", color: "white", fontWeight: "bold", fontSize: "36px" }}>
                        Pay Now
                    </button>
                </StripeCheckout>
            )}

        </div>
    )
}

export default Pay