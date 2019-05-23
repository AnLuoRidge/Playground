#!/usr/bin/perl

open INFILE, "<$ARGV[1]" or die "Failed to open file.";

# check if empty
if (-z "$ARGV[1]") {
die "No tasks found\n" ;
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


# printf "\n\n unsorted:";
# printf "$tasks[0]->{program_name} \n";
# printf "$tasks[1]->{program_name} \n";

# printf "\nsorted";
@sorted_tasks = sort { lc($a->{program_name}) cmp lc($b->{program_name}) } @tasks;

# print $tasks[2]->{program_name};

foreach (@sorted_tasks) {
	printf "$_->{process_id} $_->{memory_size} $_->{cpu_time} $_->{program_name}\n";
}