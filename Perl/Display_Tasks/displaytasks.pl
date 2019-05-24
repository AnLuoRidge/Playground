#!/usr/bin/perl
# use strict;
use Getopt::Long;
# use Switch;

# command line flags/options
# my %options=();
# GetOptions("a:m:t:s:n", \%options);

GetOptions ("all=s" => \$file,
			"file=s"   => \$data,      # string
			"verbose"  => \$verbose,   # flag
			"sreshhold=s{2}" => \@args)
or die("Error in command line arguments\n");

sort_tasks($file) if $file;
print "-s @args\n" if @args;

# sort_tasks($options{a}) if defined $options{a};
# print "-m $options{m}\n" if defined $options{m};
# print "-t $options{t}\n" if defined $options{t};
# print "-s $options{s}\n" if defined $options{s};
# print "-n $options{n}\n" if defined $options{n};

# other things found on the command line
# print "Other things found on the command line:\n" if $ARGV[0];
# foreach (@ARGV)
# {
#   print "$_\n";
# }

# switch ($ARGV[0]) {
# 	case "-a" { sort_tasks() }
# 	case "-m" { total_memory_size() }
# 	case "-t" { total_cpu_time() }
# 	case "-s" { memory_threshold($ARGV[1]) }
# 	case "-n" { student_info() }
# 	else { printf "Unknown option.\n"}
# }

sub read_file {
open INFILE, "<$_[0]" or die "Failed to open file.";

$isNull = 0;
# check if empty
if (-z "$_[0]") {
$isNull = 1;
}

# parse
@tasks;
while (<INFILE>) {
	@values = split /\s+/, $_;
	my %info = (
		process_id => $values[0],
		memory_size => $values[1],
		cpu_time => $values[2],
		program_name => $values[3]
	);
	#push @tasks, %info;
	#print $tasks[0]{CPUTime};
	push(@tasks, \%info);
	#print $tasks[0]->{program_name};
}
}

# printf "\n\n unsorted:";
# printf "$tasks[0]->{program_name} \n";
# printf "$tasks[1]->{program_name} \n";

# printf "\nsorted";


sub sort_tasks {
	read_file($_[0]);
	if ($isNull) {
		printf "No tasks found\n";
		exit;
	}
	@sorted_tasks = sort { lc($a->{program_name}) cmp lc($b->{program_name}) } @tasks;

# print $tasks[2]->{program_name};

	foreach (@sorted_tasks) {
		printf "$_->{process_id} $_->{memory_size} $_->{cpu_time} $_->{program_name}\n";
	}
}

sub total_memory_size {
	if ($isNull) {
		printf "No memory used\n";
		exit;
	}
	print "Total memory size: ???\n";
}

sub total_cpu_time {
	if ($isNull) {
		printf "No CPU time used\n";
		exit;
	}
	printf "Total CPU time: seconds\n";
}

sub memory_threshold {
	if ($isNull) {
		printf "No tasks with the specified memory size\n";
		exit;
	}

	printf "$_[0]";
}

sub student_info {
	printf "Name: Xinyuan Ding\nStudent ID: 12873687\nCompletion Date: 26 May 2019\n";
}