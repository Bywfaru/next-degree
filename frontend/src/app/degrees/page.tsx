'use client';

import clsx from 'clsx';
import { type FC } from 'react';
import { logoutUser } from '../actions';

const DegreesPage: FC = () => {
  // const cookieStore = await cookies();

  // if (!cookieStore.has('accessToken')) redirect('/login');

  return (
    <main
      className={clsx([
        'max-w-screen-lg',
        'w-full',
        'mx-auto',
        'min-h-screen',
        'flex',
        'justify-center',
        'items-center',
      ])}
    >
      <form action={logoutUser}>
        <button type="submit">Logout</button>
      </form>
    </main>
  );
};

export default DegreesPage;
