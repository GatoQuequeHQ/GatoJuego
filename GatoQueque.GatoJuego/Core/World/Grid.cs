using System.Diagnostics.CodeAnalysis;

namespace GatoQueque.GatoJuego.Core.World;

[SuppressMessage("Performance", "CA1814:Prefer jagged arrays over multidimensional")]
internal sealed class Grid : IEnumerable<Cell>
{
	private readonly Cell[,] _cells;
	private readonly Int32 _offsetX;
	private readonly Int32 _offsetY;

	public Grid(Int32 size, Int32 offsetX, Int32 offsetY)
	{
		ArgumentOutOfRangeException.ThrowIfNegative(size);
		ArgumentOutOfRangeException.ThrowIfNegative(offsetX);
		ArgumentOutOfRangeException.ThrowIfNegative(offsetY);
		ArgumentOutOfRangeException.ThrowIfGreaterThan(offsetX, size, nameof(offsetX));
		ArgumentOutOfRangeException.ThrowIfGreaterThan(offsetY, size, nameof(offsetY));

		Size = size;
		_offsetX = offsetX;
		_offsetY = offsetY;

		_cells = new Cell[size, size];
		for (var i = 0; i < size; i++)
		for (var j = 0; j < size; j++)
			_cells[i, j] = Cell.NilCell;
	}

	internal Int32 Size { get; }

	internal Int32 MinX => -_offsetX;

	internal Int32 MinY => -_offsetY;

	internal Int32 MaxX => Size - 1 - _offsetX;

	internal Int32 MaxY => Size - 1 - _offsetY;

	internal Cell this[Int32 x, Int32 y]
	{
		get => _cells[x + _offsetX, y + _offsetY];
		set => _cells[x + _offsetX, y + _offsetY] = value;
	}

	IEnumerator<Cell> IEnumerable<Cell>.GetEnumerator() => new Enumerator(_cells, Size);

	IEnumerator IEnumerable.GetEnumerator() => new Enumerator(_cells, Size);

	internal Enumerator GetEnumerator() => new(_cells, Size);

	internal struct Enumerator : IEnumerator<Cell>
	{
		private readonly Cell[,] _cells;
		private readonly Int32 _size;
		private Int32 _positionX;
		private Int32 _positionY;

		internal Enumerator(Cell[,] cells, Int32 size)
		{
			_cells = cells;
			_size = size;
			Reset();
		}

		internal readonly Cell Current => _cells[_positionX, _positionY];

		public Boolean MoveNext()
		{
			if (_positionY >= _size)
				return false;

			if (++_positionX < _size)
				return true;

			_positionX = 0;
			return ++_positionY < _size;
		}

		public void Reset()
		{
			_positionX = -1;
			_positionY = 0;
		}

		public readonly void Dispose()
		{ }

		readonly Cell IEnumerator<Cell>.Current => Current;

		readonly Object? IEnumerator.Current => Current;
	}
}
