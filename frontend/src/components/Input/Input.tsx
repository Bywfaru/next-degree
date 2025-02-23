'use client';

import { ErrorMessage } from '@hookform/error-message';
import clsx from 'clsx';
import type { DetailedHTMLProps, FC, InputHTMLAttributes } from 'react';
import type { FieldErrors, UseFormRegisterReturn } from 'react-hook-form';

export type InputProps = DetailedHTMLProps<
  InputHTMLAttributes<HTMLInputElement>,
  HTMLInputElement
> & {
  register?: UseFormRegisterReturn;
  label?: string;
  errors?: FieldErrors;
  fullWidth?: boolean;
};

export const Input: FC<InputProps> = ({
  errors,
  name,
  register,
  label,
  className,
  fullWidth,
  required,
  type = 'text',
  ...restInputProps
}) => {
  const nameToUse = register?.name || name;
  const requiredToUse = !!(register?.required ?? required);

  return (
    <label className={clsx(['flex', 'flex-col', 'w-full', 'gap-1'])}>
      {!!label && (
        <span>
          {label}
          {requiredToUse && '*'}
        </span>
      )}

      <div
        className={clsx([
          'flex',
          'flex-col',
          {
            'w-full': fullWidth,
          },
        ])}
      >
        <input
          type={type}
          className={clsx([
            'p-2.5',
            'border',
            'border-primary',
            'rounded-[4px]',
            'placeholder-text-primary/50',
            'w-full',
            className,
          ])}
          {...restInputProps}
          {...register}
        />

        {!!(errors && nameToUse) && (
          <ErrorMessage
            name={nameToUse}
            errors={errors}
            render={({ message }) => <p className="text-red-500">{message}</p>}
          />
        )}
      </div>
    </label>
  );
};
