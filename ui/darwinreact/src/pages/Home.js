import { Helmet } from "react-helmet";

export default function Home(){

    return (
        <div>
            <Helmet>
                <title> Anasayfa</title>
                <meta name="description" content="anasayfa description"/>
            </Helmet>
            <h1>Home Page</h1>
        </div>
    )
}