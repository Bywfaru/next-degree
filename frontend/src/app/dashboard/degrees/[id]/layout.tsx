'use server';

import { getAllDegrees, getDegreeById } from '@/app/actions/degrees';
import { DegreeDashboardNavbar } from '@/components/navbars';
import type { Metadata } from 'next';
import type { FC, PropsWithChildren } from 'react';

type MetadataProps = {
  params: Promise<{ id: string }>
}

type LayoutProps = {
  params: Promise<{ id: string }>;
}

export const generateMetadata = async ({ params }: MetadataProps): Promise<Metadata> => {
  const { id } = await params;
  const degree = await getDegreeById(id);

  if (!degree.success) return {
    title: 'Not Found | Next Degree',
  };

  return {
    title: `${degree.data.data.name} | Next Degree`,
  };
};

const DegreesLayout: FC<Required<PropsWithChildren<LayoutProps>>> = async ({
  children,
  params,
}) => {
  const { id } = await params;
  const getAllDegreesRes = await getAllDegrees();
  const degrees = getAllDegreesRes.success ? getAllDegreesRes.data.data : [];

  return (
    <>
      <DegreeDashboardNavbar
        degrees={degrees.map((degree) => ({
          name: degree.name,
          id: degree.id,
        }))}
        selectedDegreeId={id}
      />

      {children}
    </>
  );
};

export default DegreesLayout;
