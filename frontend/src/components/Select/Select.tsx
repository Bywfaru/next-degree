import { ErrorMessage } from '@hookform/error-message';
import clsx from 'clsx';
import type { DetailedHTMLProps, FC, SelectHTMLAttributes } from 'react';
import type { FieldErrors, UseFormRegisterReturn } from 'react-hook-form';

export type SelectOption = {
  label: string;
  value: string;
};

export type SelectProps = DetailedHTMLProps<
  SelectHTMLAttributes<HTMLSelectElement>,
  HTMLSelectElement
> & {
  register?: UseFormRegisterReturn;
  label?: string;
  errors?: FieldErrors;
  fullWidth?: boolean;
};

export const Select: FC<SelectProps> = ({
  errors,
  name,
  register,
  label,
  className,
  fullWidth,
  children,
  required,
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
        <select
          className={clsx([
            'p-2.5',
            'border',
            'border-primary',
            'rounded-[4px]',
            'placeholder-text-primary/50',
            'w-full',
            'bg-white',
            className,
          ])}
          {...restInputProps}
          {...register}
        >
          <option value="" disabled>
            Select an option
          </option>
          {!children && <option value="">No options available</option>}
          {children}
        </select>

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
