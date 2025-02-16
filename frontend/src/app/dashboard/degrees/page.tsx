'use server';

import { getAllDegrees } from '@/app/actions/degrees';
import { AddNewDegreeDashboardItem } from '@/components/AddNewDegreeDashboardItem';
import { DegreeDashboardItem } from '@/components/DegreeDashboardItem';
import clsx from 'clsx';
import { type FC } from 'react';

const DEGREES_BASE_URL = '/dashboard/degrees';

const DegreesPage: FC = async () => {
  const getAllDegreesRes = await getAllDegrees();
  const degrees = getAllDegreesRes.success ? getAllDegreesRes.data.data : [];

  return (
    <main
      className={clsx([
        'max-w-screen-lg',
        'w-full',
        'mx-auto',
        'p-5',
        'grid',
        'grid-cols-2',
        'gap-2.5',
      ])}
    >
      {degrees.map((degree) => (
        <DegreeDashboardItem
          key={degree.id}
          name={degree.name}
          status={degree.status}
          completedCredits={degree.completedCredits}
          totalCredits={degree.totalCredits}
          href={`${DEGREES_BASE_URL}/${degree.id}`}
        />
      ))}

      <AddNewDegreeDashboardItem type="link" href={`${DEGREES_BASE_URL}/new`} />
    </main>
  );
};

export default DegreesPage;
