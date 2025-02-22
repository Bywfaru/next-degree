'use client';

import clsx from 'clsx';
import { ChevronsUpDown } from 'lucide-react';
import type { ChangeEventHandler, FC, PropsWithChildren } from 'react';

type DegreeDashboardNavbarSelectProps = PropsWithChildren<{
  onChange?: ChangeEventHandler<HTMLSelectElement>
}>

export const DegreeDashboardNavbarSelect: FC<DegreeDashboardNavbarSelectProps> = ({
  children,
  onChange,
}) => {

  return <div className={clsx([
    'relative',
    'max-w-[50%]',
  ])}
  >
    <select
      className={clsx([
        'border-none',
        'bg-transparent',
        'appearance-none',
        'truncate',
        'cursor-pointer',
        'pr-4',
        'w-full',
      ])}
      onChange={onChange}
    >
      {children}
    </select>

    <ChevronsUpDown size={16}
      className={clsx([
        'text-text-primary',
        'absolute',
        'right-0',
        'top-1/2',
        '-translate-y-1/2',
        'pointer-events-none',
      ])}
    />
  </div>;

};