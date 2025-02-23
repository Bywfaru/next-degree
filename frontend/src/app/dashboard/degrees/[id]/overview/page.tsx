'use server';

import { getDegreeById } from '@/app/actions/degrees';
import { DashboardContainer } from '@/components';
import { Status } from '@/components/DegreeDashboardItem';
import clsx from 'clsx';
import { format } from 'date-fns';
import Link from 'next/link';
import { notFound } from 'next/navigation';
import type { FC } from 'react';

type PageProps = {
  params: Promise<{ id: string }>;
}

const MOCK_COURSES: {
  id: string;
  name: string;
  code: string;
  credits: number;
  status: number;
  grade: number;
  passingGrade: number;
}[] = [
  {
    id: '646e868b-98ba-4c8a-9f2a-27c38375a9c0',
    name: 'Interaction Design',
    code: 'COMP 7012',
    credits: 3,
    status: 1,
    grade: 0.92,
    passingGrade: 0.5,
  },
  {
    id: '646e868b-98ba-4c8a-9f2a-27c38375a9c1',
    name: 'Introduction to Information and Network Security',
    code: 'COMP 7003',
    credits: 3,
    status: 1,
    grade: 0.54,
    passingGrade: 0.6,
  },
  {
    id: '646e868b-98ba-4c8a-9f2a-27c38375a9c2',
    name: 'Calculus for Computing',
    code: 'MATH 7808',
    credits: 4,
    status: 1,
    grade: 0.62,
    passingGrade: 0.6,
  },
];

const MOCK_ASSIGNMENTS: {
  id: string;
  course: string;
  name: string;
  dueDate: string;
  weight: number;
}[] = [
  {
    id: '646e868b-98ba-4c8a-9f2a-27c38375a9c3',
    course: '646e868b-98ba-4c8a-9f2a-27c38375a9c0',
    name: 'Assignment 1',
    dueDate: '2022-10-15T23:59:59Z',
    weight: 0.15,
  },
];

const DegreePage: FC<PageProps> = async ({ params }) => {
  const { id } = await params;
  const getDegreeByIdRes = await getDegreeById(id);

  if (!getDegreeByIdRes.success) return notFound();

  const degree = getDegreeByIdRes.data.data;
  const percentageCompleted = degree.completedCredits / degree.totalCredits;
  const isNotStartedOrAbandoned = degree.status === Status.NOT_STARTED
    || degree.status === Status.ABANDONED;
  const courses = MOCK_COURSES; // TODO: Fetch courses
  const assignments = MOCK_ASSIGNMENTS; // TODO: Fetch assignments
  const baseUrl = `/dashboard/degrees/${id}`;

  return (
    <main
      className={clsx([
        'max-w-screen-lg',
        'w-full',
        'mx-auto',
        'min-h-screen',
        'flex',
        'flex-col',
        'p-5',
        'gap-5',
      ])}
    >
      <h1 className="sr-only">{degree.name}</h1>

      <div className={clsx(['flex', 'flex-col', 'gap-2.5'])}>
        <h2 className={clsx(['text-5xl', 'text-primary', 'font-bold'])}>
          Welcome back
        </h2>

        <p className={clsx(['flex', 'flex-col', 'leading-none'])}>
          <span className={clsx(['font-bold', 'text-xl'])}>
            Program progress:
          </span>

          <span>
            <span className={clsx([
              isNotStartedOrAbandoned && 'text-text-primary/50',
              !isNotStartedOrAbandoned && {
                'text-grade-poor': percentageCompleted <= 0.5,
                'text-grade-fair': percentageCompleted > 0.5 && percentageCompleted < 1,
                'text-grade-good': percentageCompleted >= 1,
              },
              'font-bold',
              'text-[4rem]',
            ])}
            >{degree.completedCredits}</span>/{degree.totalCredits}
          </span>

          credits earned
        </p>
      </div>

      <DashboardContainer className={clsx(['flex', 'flex-col'])}>
        <h3 className={clsx(['text-xl', 'font-bold'])}>This semester</h3>

        {!courses.length &&
          <p className={clsx(['py-5', 'text-center'])}>
            No courses to display
          </p>
        }

        <div className={clsx([
          'flex',
          'flex-col',
          '[&>*:not(:last-child)]:border-b',
          'border-primary/25',
        ])}
        >
          {courses.map((course) => {
            return <Link href={`${baseUrl}/courses/${course.id}`}
              key={course.id}
              className={clsx([
                'flex',
                'flex-col',
                'gap-2.5',
                'py-5',
                'leading-none',
                'hover:bg-primary/10',
                'transition-colors',
              ])}
            >
              <p className={clsx([
                'font-bold',
                'leading-tight',
              ])}
              >{course.code} - {course.name}</p>
              <p>Grade: <span className={clsx([
                'font-bold', {
                  'text-grade-poor': course.grade <= 0.5,
                  'text-grade-fair': course.grade > 0.5 && course.grade < 0.8,
                  'text-grade-good': course.grade >= 0.8,
                },
              ])}
              >{course.grade * 100}%</span></p>
              <p>
                Passing grade:{' '}
                <span className="font-bold">{course.passingGrade * 100}%</span>
              </p>
            </Link>;
          })}
        </div>
      </DashboardContainer>

      <DashboardContainer className={clsx(['flex', 'flex-col'])}>
        <h3 className={clsx(['text-xl', 'font-bold'])}>Upcoming</h3>

        {!assignments.length &&
          <p className={clsx(['py-5', 'text-center'])}>
            No upcoming assignments to display
          </p>
        }

        <div className={
          clsx([
            'flex',
            'flex-col',
            '[&>*:not(:last-child)]:border-b',
            'border-primary/25',
          ])}
        >
          {assignments.map((assignment) => {
            const course = courses.find((course) => course.id === assignment.course);

            if (!course) return null;

            const dueDate = new Date(assignment.dueDate);

            return <Link href={`${baseUrl}/courses/${assignment.course}/assignments/${assignment.id}`}
              key={assignment.id}
              className={clsx([
                'flex',
                'flex-col',
                'gap-2.5',
                'py-5',
                'leading-none',
                'hover:bg-primary/10',
                'transition-colors',
              ])}
            >
              <p className={clsx([
                'font-bold',
                'leading-tight',
              ])}
              >{course.code} - {course.name}</p>
              <p>{assignment.name}</p>
              <p className="text-grade-poor">
                Due{' '}
                {format(dueDate, 'MMM d, y')}
                at{' '}
                {format(dueDate, 'h:m aa')}
              </p>
              <p>
                Weight:{' '}
                <span className="font-bold">{assignment.weight * 100}%</span>
              </p>
            </Link>;
          })}
        </div>
      </DashboardContainer>
    </main>
  );
};

export default DegreePage;