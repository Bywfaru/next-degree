'use server';

import { LoginForm } from '@/components/forms';
import clsx from 'clsx';
import { cookies } from 'next/headers';
import { redirect } from 'next/navigation';
import { type FC } from 'react';

const LoginPage: FC = async () => {
  const cookieStore = await cookies();

  if (cookieStore.has('accessToken')) redirect('/dashboard/degrees');

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
      <div
        className={clsx([
          'flex',
          'flex-col',
          'w-full',
          'p-5',
          'gap-4',
          'lg:flex-row-reverse',
          'lg:gap-20',
        ])}
      >
        <div
          className={clsx([
            'flex',
            'flex-col',
            'w-full',
            'gap-2.5',
            'lg:gap-5',
          ])}
        >
          <h1
            className={clsx([
              'font-bold',
              'text-4xl',
              'text-primary',
              'lg:text-6xl',
            ])}
          >
            Login
          </h1>

          <p>
            Login to view your degree progress, course grades, and assignment
            due dates.
          </p>
        </div>

        <LoginForm />
      </div>
    </main>
  );
};

export default LoginPage;
