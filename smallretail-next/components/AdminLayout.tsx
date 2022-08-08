import { NextPage } from "next";
import { useRouter } from "next/router";
import { Menubar } from "primereact/menubar";
import Layout from "./Layout";

interface AdminLayoutProps {
    pageTitle: string;
    children: React.ReactNode;
}

const AdminLayout: NextPage<AdminLayoutProps> = ({ pageTitle, children }) => {
    const router = useRouter();
    const items = [
        { 
            label: "Home",
            command: () => router.push('/dashboard')
        },
        { 
            label: "Manage",
            items: [
                { 
                    label: "Products",
                    command: () => router.push('/products')
                },
                { label: "Users"}
            ]
        }
    ]
    return (
        <Layout title={`${pageTitle} - SmallRetail Admin`}>
            <Menubar model={items} />
            {children}
        </Layout>
    )
}

export default AdminLayout;