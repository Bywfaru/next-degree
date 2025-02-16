'use server';

import { CreateDegreeForm } from '@/components/forms';
import clsx from 'clsx';
import type { FC } from 'react';

const NewDegreeLayout: FC = async () => {
  return (
    <main
      className={clsx([
        'max-w-screen-lg',
        'w-full',
        'mx-auto',
        'p-5',
        'flex',
        'flex-col',
        'gap-10',
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
        Create a new degree
      </h1>

      <CreateDegreeForm />
    </main>
  );
};

export default NewDegreeLayout;
