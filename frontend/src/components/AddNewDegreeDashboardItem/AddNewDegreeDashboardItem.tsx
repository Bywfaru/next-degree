import clsx from 'clsx';
import { Plus } from 'lucide-react';
import type { FC } from 'react';

export type AddNewDegreeDashboardItemProps = {
  onClick?: () => void;
}
export const AddNewDegreeDashboardItem: FC<AddNewDegreeDashboardItemProps> = ({
  onClick,
}) => {
  return <button onClick={onClick}
    className={clsx([
      'flex',
      'flex-col',
      'items-center',
      'justify-center',
      'gap-2.5',
      'w-full',
      'min-h-[170px]',
      'px-2.5',
      'py-5',
      'border',
      'border-primary',
      'rounded-lg',
      'h-full',
      'border-dashed',
      'transition',
      'text-primary',
      'border-2',
    ])}
  >
    <p>Add a new degree</p>

    <Plus size={24} />
  </button>;
};