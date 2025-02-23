'use client';

import clsx from 'clsx';
import type { DetailedHTMLProps, FC, HTMLAttributes } from 'react';

export type DashboardContainerProps = DetailedHTMLProps<HTMLAttributes<HTMLDivElement>, HTMLDivElement>;

export const DashboardContainer: FC<DashboardContainerProps> = ({
  className,
  ...restProps
}) => {
  return <div className={clsx([
    'px-2.5',
    'py-5',
    'bg-white',
    'border',
    'border-primary',
    'rounded-lg',
    className,
  ])} {...restProps} />;
};