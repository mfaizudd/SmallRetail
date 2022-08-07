import { NextPage } from "next";
import Head from "next/head";

interface LayoutProps {
    children: React.ReactNode;
    title?: string;
}

const Layout: NextPage<LayoutProps> = (props) => {
    return (
        <div className="surface-200">
            <Head>
                <title>{ props.title }</title>
                <meta name="description" content="SmallRetail" />
                <link rel="icon" href="/favicon.ico" />
            </Head>
            { props.children }
        </div>
    )
}
export default Layout;