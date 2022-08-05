import type { NextPage } from 'next';
import Head from 'next/head';
import { Button } from 'primereact/button';
import { InputText } from 'primereact/inputtext';
import { Checkbox } from 'primereact/checkbox';
import { useState } from 'react';
// import Image from 'next/image';

const Home: NextPage = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [checked, setChecked] = useState(false);
  return (
    <div className="surface-200">
      <Head>
        <title>SmallRetail App</title>
        <meta name="description" content="SmallRetail" />
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <div className="flex align-items-center justify-content-center min-h-screen">

        <div className="surface-card p-4 shadow-2 border-round w-full lg:w-6">
          <div className="text-center mb-5">
            {/* <Image src="assets/images/blocks/logos/hyper.svg" alt="hyper" height={50} className="mb-3" /> */}
            <div className="text-900 text-3xl font-medium mb-3">Welcome to SmallRetail</div>
            <span className="text-600 font-medium line-height-3">{ "Don't have an account? Contact administrator" }</span>
          </div>

          <div>
            <label htmlFor="username" className="block text-900 font-medium mb-2">Username</label>
            <InputText id="username" type="text" className="w-full mb-3" value={username} onChange={e => setUsername(e.target.value)} />

            <label htmlFor="password" className="block text-900 font-medium mb-2">Password</label>
            <InputText id="password" type="password" className="w-full mb-3" value={password} onChange={e => setPassword(e.target.value)} />

            <div className="flex align-items-center justify-content-between mb-6">
              <div className="flex align-items-center">
                <Checkbox id="rememberme" onChange={e => setChecked(e.checked)} checked={checked} className="mr-2" />
                <label htmlFor="rememberme">Remember me</label>
              </div>
              <a className="font-medium no-underline ml-2 text-blue-500 text-right cursor-pointer">Forgot your password?</a>
            </div>

            <Button label="Sign In" icon="pi pi-user" className="w-full" />
          </div>
        </div>
      </div>
    </div>
  )
}

export default Home
