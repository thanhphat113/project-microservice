resource "aws_vpc" "this" {
  cidr_block           = var.vpc_cidr
  enable_dns_hostnames = true
  enable_dns_support   = true

  tags = merge(var.tags, {
    Name = "${var.project}-vpc-${var.env}-${var.aws_region}"
  })

}

resource "aws_internet_gateway" "this" {
  vpc_id = aws_vpc.this.id

  tags = merge(var.tags, {
    Name = "${var.project}-igw-${var.env}"
  })
}

resource "aws_subnet" "public" {
  for_each = toset(var.public_subnets)

  vpc_id                  = aws_vpc.this.id
  cidr_block              = each.value
  availability_zone       = element(var.azs, index(var.public_subnets, each.value))
  map_public_ip_on_launch = true

  tags = merge(var.tags, { Name = "${var.project}-subnet-public-${var.env}-${element(var.azs, index(var.public_subnets, each.value))}" })
}

resource "aws_subnet" "private" {
  for_each = toset(var.private_subnets)

  vpc_id                  = aws_vpc.this.id
  cidr_block              = each.key
  availability_zone       = element(var.azs, index(var.private_subnets, each.key))
  map_public_ip_on_launch = true

  tags = merge(var.tags, { Name = "${var.project}-subnet-private-${var.env}-${element(var.azs, index(var.private_subnets, each.key))}" })
}

resource "aws_eip" "nat" {
  domain = "vpc"
}

resource "aws_nat_gateway" "this" {
  allocation_id = aws_eip.nat.id
  subnet_id     = element(values(aws_subnet.public)[*].id, 0)

  tags = merge(var.tags, { Name = "${var.project}-nat-${var.env}" })
}

resource "aws_route_table" "public" {
  vpc_id = aws_vpc.this.id

  route {
    cidr_block = "0.0.0.0/0"
    gateway_id = aws_internet_gateway.this.id
  }

  tags = merge(var.tags, { Name = "${var.project}-rt-public-${var.env}" })
}

resource "aws_route_table_association" "public" {
  for_each       = aws_subnet.public
  subnet_id      = each.value.id
  route_table_id = aws_route_table.public.id
}

resource "aws_route_table" "private" {
  vpc_id = aws_vpc.this.id
  route {
    cidr_block     = "0.0.0.0/0"
    nat_gateway_id = aws_nat_gateway.this.id
  }
  tags = merge(var.tags, { Name = "${var.project}-rt-private-${var.env}" })
}

resource "aws_route_table_association" "private" {
  for_each       = aws_subnet.private
  subnet_id      = each.value.id
  route_table_id = aws_route_table.private.id
}

