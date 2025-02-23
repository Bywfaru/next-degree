'use client';

import clsx from 'clsx';
import { Menu } from 'lucide-react';
import type { FC } from 'react';

export const DashboardNavbar: FC = () => {
  return <nav className={clsx([
    'px-2.5',
    'py-5',
    'border-b',
    'border-primary/50',
    'text-primary',
    'flex',
    'justify-between',
  ])}
  >
    <h1>Dashboard</h1>

    <Menu size={24} className={clsx(['text-text-primary'])} />
  </nav>;
};