'use client';

import clsx from 'clsx';
import { Loader } from 'lucide-react';
import type { ButtonHTMLAttributes, DetailedHTMLProps, FC } from 'react';

export type ButtonProps =
  DetailedHTMLProps<ButtonHTMLAttributes<HTMLButtonElement>, HTMLButtonElement>
  & {
  loading?: boolean;
}

export const Button: FC<ButtonProps> = ({
  className,
  disabled,
  loading,
  children,
  ...restProps
}) => {
  const isDisabled = disabled || loading;

  return <button className={clsx([
    'flex',
    'justify-center',
    'gap-2.5',
    'items-center',
    'rounded-sm',
    'border',
    'border-primary',
    'text-white',
    'px-4',
    'py-2',
    'bg-primary',
    'enabled:hover:bg-transparent',
    'enabled:hover:text-primary',
    'transition',
    'disabled:bg-primary/50',
    'disabled:border-primary/50',
    'disabled:cursor-not-allowed',
    className,
  ])} disabled={isDisabled} {...restProps}>
    {loading && <Loader size={16} className="animate-spin" />}

    <div>{children}</div>
  </button>;
};