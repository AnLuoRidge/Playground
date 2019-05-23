#!/bin/bash

printf "=== FAIL TO OPEN FILE (EMPTY) ===\n"
assert=$(./displaytasks.pl -a ./taskfile_empty.txt 2>&1)
echo $assert
if [ "$assert" = "No tasks found" ] ; then
	printf "[PASS]\n"
else
	printf "[FAIL]\n";
fi

printf "\n=== UNREADABLE ===\n"
assert=$(./displaytasks.pl -a ./taskfile_unreadable.txt 2>&1)
echo $assert
if [ "$assert" = "Failed to open file. at ./displaytasks.pl line 3." ] ; then
	printf "[PASS]\n"
else
	printf "[FAIL]\n"
fi

printf "\n=== PRINT ALL ===\n"
assert=`./displaytasks.pl -a ./taskfile_normal.txt`
# echo -n "$assert" | hd
correct=`printf "4 0 6 events\n22355 1970 2009 find\n1 4730 1031782 init\n21413 5928 1 sshd\n2190 450 0 top"`
# echo -n "$correct" | hd
# $? last output
if [ "$assert" = "$correct" ] ; then
	printf "[PASS]\n"
else
	printf "[FAIL]\n"
	printf "$assert\n\nShould be:\n$correct\n"
fi