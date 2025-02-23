'use client';

import clsx from 'clsx';
import { Plus } from 'lucide-react';
import Link from 'next/link';
import type { FC } from 'react';

export type AddNewDegreeDashboardItemProps =
  | {
  type: 'button';
  onClick?: () => void;
}
  | {
  type: 'link';
  href: string;
};

const AddNewDegreeDashboardItemChildren: FC = () => {
  return (
    <>
      <p>Add a new degree</p>

      <Plus size={24} />
    </>
  );
};

const CLASS_NAME = clsx([
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
]);

export const AddNewDegreeDashboardItem: FC<AddNewDegreeDashboardItemProps> = (
  props,
) => {
  switch (props.type) {
    case 'button': {
      const { onClick } = props;

      return (
        <button onClick={onClick} className={CLASS_NAME}>
          <AddNewDegreeDashboardItemChildren />
        </button>
      );
    }
    case 'link': {
      const { href } = props;

      return (
        <Link href={href} className={CLASS_NAME}>
          <AddNewDegreeDashboardItemChildren />
        </Link>
      );
    }
    default: {
      return null;
    }
  }
};
