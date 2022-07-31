import type { NextPage } from 'next';
import Head from 'next/head';
import { Card } from 'primereact/card';
import { Button } from 'primereact/button';
import { InputText } from 'primereact/inputtext';
import { useState } from 'react';

const Home: NextPage = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  return (
    <div className="flex p-5 justify-content-center">
      <Head>
        <title>SmallRetail App</title>
        <meta name="description" content="SmallRetail" />
        <link rel="icon" href="/favicon.ico" />
      </Head>
      <Card title="Login" className="min-w-min w-25rem">
        <div className="flex flex-column gap-3">
          <span className="p-float-label">
            <InputText id="in" value={username} onChange={(e) => setUsername(e.target.value)} className="w-full" />
            <label htmlFor="in">Username</label>
          </span>
          <span className="p-float-label">
            <InputText id="in" value={password} onChange={(e) => setPassword(e.target.value)} className="w-full" type="password" />
            <label htmlFor="in">Password</label>
          </span>
          <Button label="Login" />
        </div>
      </Card>
    </div>
  )
}

export default Home
