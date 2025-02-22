'use client';

import {
  DegreeDashboardNavbarSelect,
} from '@/components/navbars/DegreeDashboardNavbar/components';
import clsx from 'clsx';
import { Menu } from 'lucide-react';
import Link from 'next/link';
import { usePathname, useRouter } from 'next/navigation';
import type { ChangeEventHandler, FC } from 'react';

const DegreePage: Record<string, {
  name: string;
  pathname: string;
}> = {
  OVERVIEW: {
    name: 'Overview',
    pathname: '/overview',
  },
  COURSES: {
    name: 'Courses',
    pathname: '/courses',
  },
  ROADMAP: {
    name: 'Roadmap',
    pathname: '/roadmap',
  },
};

type DegreeDashboardNavbarProps = {
  degrees: {
    id: string;
    name: string;
  }[];
  selectedDegreeId: string;
}

export const DegreeDashboardNavbar: FC<DegreeDashboardNavbarProps> = ({
  selectedDegreeId,
  degrees,
}) => {
  const router = useRouter();
  const pathname = usePathname();
  const selectedDegree = degrees.find((degree) => degree.id === selectedDegreeId);
  const degreesWithoutSelected = degrees.filter((degree) => degree.id !== selectedDegreeId);
  const baseUrl = `/dashboard/degrees/${selectedDegreeId}`;

  const handleSelectChange: ChangeEventHandler<HTMLSelectElement> = (event) => {
    const { value } = event.target;

    router.push(`/dashboard/degrees/${value}/overview`);
  };

  return <nav className={
    clsx([
      'px-2.5',
      'pt-5',
      'border-b',
      'border-primary/50',
      'text-primary',
      'flex',
      'flex-col',
      'gap-2.5',
    ])}
  >
    <h1 className={clsx(['sr-only'])}>{selectedDegree?.name}</h1>

    <div className={clsx(['flex', 'justify-between'])}>
      <DegreeDashboardNavbarSelect onChange={handleSelectChange}>
        <option value={selectedDegreeId}>{selectedDegree?.name}</option>

        {degreesWithoutSelected.map((degree) => (
          <option key={degree.id} value={degree.id}>{degree.name}</option>
        ))}
      </DegreeDashboardNavbarSelect>

      <Menu size={24} />
    </div>

    <ul className={clsx(['flex', 'list-none', 'm-0', 'p-0', 'mb-[-1px]'])}>
      {Object.values(DegreePage).map((link) => {
        const isActive = pathname.endsWith(link.pathname);

        return (
          <Link key={link.pathname}
            href={`${baseUrl}${link.pathname}`}
            className={clsx([
              isActive && ['border-b', 'border-primary', 'text-primary'],
              !isActive && ['text-primary/50'],
              'hover:text-primary',
              'py-2',
              'px-2.5',
            ])}
          >
            {link.name}
          </Link>
        );
      })}
    </ul>
  </nav>;
};